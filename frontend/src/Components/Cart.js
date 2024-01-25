// Cart.js
import React from 'react';

const Cart = ({ cartItems, onCheckout  }) => {
  const totalPrice = cartItems.reduce((total, item) => total + item.cena * item.quantity, 0);

  return (
    <div className="cart">
      <h2>Koszyk</h2>
      {cartItems.map((item, index) => (
  <div key={`${item.id_pizza}_${index}`} className="cart-item">
    <h4>{item.nazwa}</h4>
    <p>Ilość: {item.quantity}</p>
    <p>Cena: {(item.cena * item.quantity).toFixed(2)} zł</p>
  </div>
))}
      <div className="cart-total">
        <h4>Całkowita kwota: {totalPrice.toFixed(2)} zł</h4>
        <button onClick={onCheckout}>Przejdź do zamówienia</button>
      </div>
    </div>
  );
};

export default Cart;
