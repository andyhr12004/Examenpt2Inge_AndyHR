import axios from 'axios';

const API_BASE = 'https://localhost:7107/api/vendingmachine'; 

export const getAvailableDrinks = async () => {
  const response = await axios.get(`${API_BASE}/products`);
  return response.data;
};

export const buyDrinkById = async (payload) => {
  const response = await axios.post(`${API_BASE}/buy`, payload);
  return response.data;
};

