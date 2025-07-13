<template>
  <div class="vending-machine">
    <h1>Máquina expendedora</h1>

    <div class="content" :class="{ 'grid-layout': hasCart }">
      <div class="drinks-grid">
        <div
          v-for="drink in drinks"
          :key="drink.id"
          class="drink-card"
        >
          <img
            :src="getImage(drink.name)"
            :alt="drink.name"
            class="drink-img-large"
          />
          <div class="drink-info">
            <h2 class="drink-name">{{ drink.name }}</h2>
            <p class="drink-price">₡ {{ drink.price }}</p>
            <p class="drink-qty">Cantidad Disponible: {{ drink.quantity }}</p>
            <button @click="addToCart(drink.id)" class="buy-button">
              Agregar
            </button>
            <p v-if="cart[drink.id] !== undefined" class="selected-qty">
              Seleccionados: {{ cart[drink.id] }}
            </p>
          </div>
        </div>
      </div>

      <aside class="cart-summary" v-if="hasCart">
        <h2>Lista de compra</h2>
        <ul>
          <li v-for="(qty, id) in cart" :key="id">
            {{ findName(id) }} × {{ qty }} → ₡ {{ findPrice(id) * qty }}
          </li>
        </ul>
        <p class="total-cost"><strong>Total: ₡ {{ totalCost }}</strong></p>
        <button @click="checkout" class="checkout-button">
          Confirmar compra
        </button>

       
        <div v-if="changeInfo" class="change-result">
          <h3>Su vuelto es ₡{{ changeInfo.change }}</h3>
          <p>Desglose:</p>
          <ul>
            <li v-for="(qty, coin) in changeInfo.breakdown" :key="coin">
              {{ qty }} moneda{{ qty > 1 ? 's' : '' }} de ₡{{ coin }}
            </li>
          </ul>
        </div>
      </aside>
    </div>

    <div class="money-input">
      <h2>Insertar dinero</h2>
      <div class="money-buttons">
        <button @click="insertMoney(25)">₡25</button>
        <button @click="insertMoney(50)">₡50</button>
        <button @click="insertMoney(100)">₡100</button>
        <button @click="insertMoney(500)">₡500</button>
        <button @click="insertMoney(1000)">₡1000 (billete)</button>
      </div>
      <p><strong>Total ingresado:</strong> ₡{{ insertedMoney }}</p>
    </div>
  </div>
</template>

<script>
import { getAvailableDrinks, buyDrinkById } from '@/services/beverageService';

export default {
  data() {
    return {
      drinks: [],
      cart: {},
      insertedMoney: 0,
      processing: false,
      changeInfo: null, 
    };
  },
  computed: {
    totalCost() {
      return Object.entries(this.cart).reduce((sum, [id, qty]) => {
        const d = this.drinks.find(x => x.id === +id);
        return sum + (d ? d.price * qty : 0);
      }, 0);
    },
    hasCart() {
      return Object.keys(this.cart).length > 0;
    }
  },
  async mounted() {
    await this.loadDrinks();
  },
  methods: {
    getImage(name) {
      switch (name.toLowerCase()) {
        case 'coca cola': return '/images/pikachu.png';
        case 'sprite':    return '/images/Victiny.png';
        case 'pepsi':     return '/images/Snivy.png';
        case 'fanta':     return '/images/Mew.png';
        default:          return '/images/default.png';
      }
    },
    async loadDrinks() {
      try {
        this.drinks = await getAvailableDrinks();
      } catch (error) {
        console.error('Error al cargar los refrescos:', error);
      }
    },
    addToCart(id) {
      const available = this.drinks.find(x => x.id === id)?.quantity || 0;
      const current = this.cart[id] || 0;
      if (current < available) {
        this.cart[id] = current + 1;
      } else {
        alert('No puedes seleccionar más unidades que las disponibles.');
      }
    },
    findName(id) {
      const d = this.drinks.find(x => x.id === +id);
      return d ? d.name : '';
    },
    findPrice(id) {
      const d = this.drinks.find(x => x.id === +id);
      return d ? d.price : 0;
    },
    insertMoney(amount) {
      this.insertedMoney += amount;
    },
    async checkout() {
      if (this.processing) return;
      this.processing = true;

      const [drinkId] = Object.keys(this.cart);
      const quantity = this.cart[drinkId];

      const total = this.totalCost;
      if (this.insertedMoney < total) {
        alert(`Saldo insuficiente. Faltan ₡${total - this.insertedMoney}`);
        this.processing = false;
        return;
      }

      try {
        const response = await buyDrinkById({
          
          drinkId: parseInt(drinkId),
          quantity,
          insertedMoney: this.insertedMoney
        });
        console.log('RESPUESTA DEL BACKEND:', response);

        if (response.success) {
          this.changeInfo = {
            change: response.change,
            breakdown: response.changeBreakdown || {}
          };
            alert('✅ Compra realizada con éxito');
        } else {
          alert(response.message || 'La compra no fue exitosa.');
        }
      } catch (error) {
        if (error.response?.status === 503) {
          alert('⚠ Fuera de servicio: no hay cambio disponible.');
        } else {
          alert(error.response?.data?.message || 'Error al procesar la compra.');
        }
      } finally {
        this.cart = {};
        this.insertedMoney = 0;
        this.processing = false;
        await this.loadDrinks();
      }
    }
  }
};
</script>

<style scoped>
.vending-machine {
  padding: 1.5rem;
  border-radius: 1rem;
  background: linear-gradient(135deg, #fbc2eb, #a6c1ee, #b2fefa);
  font-family: sans-serif;
}
.vending-machine h1 {
  text-align: center;
  margin-bottom: 1rem;
  color: #4a5568;
}

.money-input {
  margin-bottom: 1.5rem;
  text-align: center;
}
.money-buttons {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 0.5rem;
}
.money-buttons button {
  padding: 0.5rem 1rem;
  background-color: #f6ad55;
  color: white;
  border: none;
  border-radius: 0.5rem;
  cursor: pointer;
}
.money-buttons button:hover {
  background-color: #dd6b20;
}

.content {
  display: block;
}
@media (min-width: 768px) {
  .content.grid-layout {
    display: grid;
    grid-template-columns: 3fr 1fr;
    gap: 1rem;
    align-items: start;
  }
}

.drinks-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  gap: 1rem;
}
.drink-card {
  background: #fff;
  border-radius: 1.5rem;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  padding: 1rem;
  text-align: center;
  transition: transform .2s;
}
.drink-card:hover {
  transform: translateY(-4px);
}
.drink-img-large {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border-radius: 1rem;
  margin-bottom: 0.5rem;
}
.drink-info .drink-name {
  margin: 0;
  font-size: 1.1rem;
  color: #2d3748;
}
.drink-info p {
  margin: 0.25rem 0;
  color: #4a5568;
}
.buy-button {
  margin-top: 0.5rem;
  padding: 0.4rem 0.8rem;
  background-color: #3182ce;
  color: white;
  border: none;
  border-radius: 0.5rem;
  cursor: pointer;
  transition: background-color 0.2s;
}
.buy-button:hover {
  background-color: #2c5282;
}
.selected-qty {
  margin-top: 0.25rem;
  color: #2b6cb0;
}

.cart-summary {
  margin-top: 2rem;
  padding: 1rem;
  background: #edf2f7;
  border-radius: 0.75rem;
}
.cart-summary h2 {
  margin-top: 0;
  color: #2d3748;
}
.cart-summary ul {
  list-style: none;
  padding: 0;
  margin: 0.5rem 0;
}
.cart-summary li {
  margin: 0.25rem 0;
  color: #4a5568;
}
.total-cost {
  margin-top: 0.5rem;
}
.checkout-button {
  margin-top: 1rem;
  padding: 0.5rem 1rem;
  background-color: #38a169;
  color: white;
  border: none;
  border-radius: 0.5rem;
  cursor: pointer;
  transition: background-color 0.2s;
}
.checkout-button:hover {
  background-color: #2f855a;
}
</style>
