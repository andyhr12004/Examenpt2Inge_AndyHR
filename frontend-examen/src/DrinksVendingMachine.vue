<template>
  <div class="vending-machine">
    <h1>Refrescos disponibles</h1>
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
          <button @click="buyDrink(drink.id)" class="buy-button">
            Comprar
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getAvailableDrinks, buyDrinkById } from '@/services/beverageService';

export default {
  data() {
    return {
      drinks: [],
    };
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
    async buyDrink(id) {
      try {
        await buyDrinkById(id);
        await this.loadDrinks();
        alert('¡Compra realizada con éxito!');
      } catch (error) {
        alert('No se pudo completar la compra.');
        console.error(error);
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
</style>
