import React, { useState, useEffect } from 'react';
import PizzaCard from './PizzaCard';
import axios from 'axios';
import pizzaImages from './pizzas'; // Załóżmy, że plik pizzas.js zawiera obiekty z URL-ami obrazów

const PizzaList = ({ addToCart }) => {
  const [pizzas, setPizzas] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7286/api/Pizza')
      .then(response => {
        // Odwołanie bezpośrednio do właściwości `$values` w odpowiedzi.
        const updatedPizzas = response.data.$values.map(pizza => ({
          ...pizza,
          imageUrl: pizzaImages[pizza.id] // Poprawne dopasowanie do kluczy w obiekcie pizzaImages
        }));
        setPizzas(updatedPizzas);
      })
      .catch(error => {
        console.error("Wystąpił błąd podczas pobierania danych pizzy", error);
      });
  }, []);
  

  return (
    <div className="pizza-list">
      {pizzas.map((pizza, index) => (
        <PizzaCard key={`${pizza.id_pizza}_${index}`} pizza={pizza} addToCart={addToCart} />
      ))}
    </div>
  );
};

export default PizzaList;
