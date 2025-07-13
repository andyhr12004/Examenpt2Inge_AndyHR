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
          alt="pokemon"
          class="drink-img-large"
        />
        <div class="drink-info">
          <h2 class="drink-name">{{ drink.name }}</h2>
          <p class="drink-price">₡ {{ drink.price }}</p>
          <p class="drink-qty">Cantidad Disponible: {{ drink.quantity }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getAvailableDrinks } from '@/services/beverageService';

export default {
  data() {
    return { drinks: [] };
  },
  async mounted() {
    this.drinks = await getAvailableDrinks();
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
  },
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
  /* subimos el radio de 0.75rem a 1.5rem */
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
  /* de 50% a 1rem para un círculo aún más suave */
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
</style>
