// CartItem.js
import React from 'react';

const CartItem = ({ item }) => {
  return (
    <div className="cart-item">
      <img src={item.imageUrl} alt={item.nazwa} className="cart-item-image" />
      <div>
        <h4>{item.nazwa}</h4>
        <p>Ilość: {item.quantity}</p>
        <p>Cena: {item.cena.toFixed(2)} zł</p>
      </div>
    </div>
  );
};

export default CartItem;
