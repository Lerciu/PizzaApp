// OrderForm.js
import React, { useState } from 'react';
// OrderForm.js
import './OrderForm.css'; // Dodaj tę linię na górze pliku




const OrderForm = ({ onSubmit, onCancel }) => {
  const [imie, setImie] = useState('');
  const [nazwisko, setNazwisko] = useState('');
  const [numertelefonu, setNumerTelefonu] = useState('');
  const [email, setEmail] = useState('');
  const [adres, setAdres] = useState('');
  const [numerDomu, setNumerDomu] = useState('');
  const [miasto, setMiasto] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    const orderData = {
      imie, nazwisko, numertelefonu, email, adres, numerDomu, miasto
    };
    onSubmit(orderData);
  };


  return (
    <form className="order-form" onSubmit={handleSubmit}>
      <h2>Formularz zamówienia</h2>
      <input type="text" value={imie} onChange={(e) => setImie(e.target.value)} placeholder="Imię" required />
      <input type="text" value={nazwisko} onChange={(e) => setNazwisko(e.target.value)} placeholder="Nazwisko" required />
      <input type="text" value={numertelefonu} onChange={(e) => setNumerTelefonu(e.target.value)} placeholder="Numer telefonu" required />
      <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" required />
      <input type="text" value={adres} onChange={(e) => setAdres(e.target.value)} placeholder="Adres" required />
      <input type="text" value={numerDomu} onChange={(e) => setNumerDomu(e.target.value)} placeholder="Numer domu/mieszkania" required />
      <input type="text" value={miasto} onChange={(e) => setMiasto(e.target.value)} placeholder="Miasto" required />
      <button type="submit">Złóż zamówienie</button>
      <button type="button" onClick={onCancel}>Anuluj</button>
    </form>
  );
};

export default OrderForm;
