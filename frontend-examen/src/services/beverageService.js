import axios from 'axios';

const API_BASE = 'https://localhost:7107/api/vendingmachine'; 

export const getAvailableDrinks = async () => {
  const response = await axios.get(`${API_BASE}/products`);
  return response.data;
};

export const buyDrinkById = async (id) => {
  const response = await axios.post(`${API_BASE}/buy/${id}`);
  return response.data;
};

