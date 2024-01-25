import React, { useState } from 'react';
import './PizzaCard.css';

const PizzaCard = ({ pizza, addToCart }) => {
  const [quantity, setQuantity] = useState(1);

  const decreaseQuantity = () => {
    setQuantity(prev => prev > 1 ? prev - 1 : 1);
  };

  const increaseQuantity = () => {
    setQuantity(prev => prev + 1);
  };

  return (
    <div className="pizza-card">
      <img src={pizza.imageUrl} alt={pizza.nazwa} className="pizza-image" />
      <h3>{pizza.nazwa}</h3>
      <p>{pizza.opis}</p>
      <div className="price">{`${pizza.cena ? pizza.cena.toFixed(2) : 'N/A'}zł`}</div>
      <div className="quantity-controls">
        <button onClick={decreaseQuantity} className="quantity-btn">-</button>
        <span className="quantity">{quantity}</span>
        <button onClick={increaseQuantity} className="quantity-btn">+</button>
      </div>
      <div className="actions">
        {pizza.isSoldOut ? (
          <div className="sold-out">WYPRZEDANE</div>
        ) : (
          <>
        
<button onClick={() => addToCart({...pizza, id_pizza: pizza.id_pizza, quantity})} className="add-to-cart">DODAJ DO KOSZYKA</button>


          </>
        )}
      </div>
    </div>
  );
};

export default PizzaCard;
