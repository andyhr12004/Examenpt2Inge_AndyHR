import axios from 'axios';

const API_URL = 'https://localhost:7107/api/beveragemachine/products';

export const getAvailableDrinks = async () => {
  const response = await axios.get(API_URL);
  return response.data;
};
