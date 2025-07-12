<template>
  <div class="container mt-5">
    <h1>Refrescos disponibles</h1>
    <table class="table mt-4">
      <thead>
        <tr>
          <th>Refresco</th>
          <th>Precio (₡)</th>
          <th>Cantidad disponible</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="drink in drinks" :key="drink.id">
          <td>{{ drink.name }}</td>
          <td>{{ drink.price }}</td>
          <td>{{ drink.quantity }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { getAvailableDrinks } from '@/services/beverageService';

export default {
  data() {
    return {
      drinks: [],
    };
  },
  async mounted() {
    try {
      this.drinks = await getAvailableDrinks();
    } catch (error) {
      console.error('Error al obtener refrescos:', error);
    }
  },
};
</script>
