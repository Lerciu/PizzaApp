// CartBar.js
import React from 'react';
import './CartBar.css'; // Upewnij się, że masz plik CartBar.css z odpowiednimi stylami

const CartBar = ({ cartItems, total, onOpenCart }) => {
  return (
    <div className={`cart-bar ${cartItems.length > 0 ? 'visible' : ''}`}>
      <span>{cartItems.length} item(s) - Total: {total} zł</span>
      <button onClick={onOpenCart}>Open Cart</button>
    </div>
  );
};

export default CartBar;
