// api.js
import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:7286/api' // Twoje API
});

export default instance;
