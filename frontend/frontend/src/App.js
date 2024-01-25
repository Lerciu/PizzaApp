import React, { useState} from 'react';
import './App.css';
import Header from './Components/Header';
import PizzaList from './Components/PizzaList';
import Cart from './Components/Cart';
import NotificationBar from './Components/NotificationBar';
import OrderForm from './Components/OrderForm';
import axios from 'axios';


function App() {
  const [cartItems, setCartItems] = useState([]);
  const [showNotification, setShowNotification] = useState(false);
  const [notificationMessage, setNotificationMessage] = useState('');
  const [isOrdering, setIsOrdering] = useState(false); // Nowy stan dla kontroli widoku formularza
  



 

  const addToCart = (pizzaToAdd) => {
    let newCart = [...cartItems];
    let itemInCart = newCart.find(item => item.id === pizzaToAdd.id); // Użyj właściwego identyfikatora 'id' zamiast 'id_pizza'
  
    if (itemInCart) {
      // Jeśli pizza już jest w koszyku, zwiększ tylko jej ilość
      itemInCart.quantity += pizzaToAdd.quantity;
    } else {
      // Jeśli nie, dodaj nową pizzę do koszyka
      newCart.push({ ...pizzaToAdd, quantity: pizzaToAdd.quantity });
    }
  
    setCartItems(newCart); // Aktualizuj stan koszyka
    setNotificationMessage(`Dodano ${pizzaToAdd.quantity} x ${pizzaToAdd.nazwa} do koszyka.`);
    setShowNotification(true);
    setTimeout(() => setShowNotification(false), 3000);
  };
  

  const handleCheckout = () => {
    setIsOrdering(true); // Pokaż formularz zamówienia
  };

 
     
  const handleSubmitOrder = (orderDetails) => {
    const fixedWorkerId = 2; // Ustawiamy stałe ID pracownika
    console.log('Submitting order with worker ID:', fixedWorkerId);
  
    const zamowienieData = {
      data: new Date().toISOString(),
      klient: {
        imie: orderDetails.imie,
        nazwisko: orderDetails.nazwisko,
        numertelefonu: orderDetails.numertelefonu,
        email: orderDetails.email,
        adres: orderDetails.adres,
        numerdomu: orderDetails.numerDomu,
        miasto: orderDetails.miasto
      },
      id_pracownik: fixedWorkerId, 
      pizzaZamowienie: cartItems.map(item => ({
        idPizza: item.id,
        ilosc: item.quantity
      })),
    };
  
    console.log('Submitting order data:', zamowienieData);
    axios.post('https://localhost:7286/api/Zamowienia', zamowienieData)
      .then(response => {
        console.log('Order created:', response.data);
        setCartItems([]); // Clear the cart after order creation
        setIsOrdering(false); // Close the order form
        setNotificationMessage('Order placed successfully.');
        setShowNotification(true);
        setTimeout(() => setShowNotification(false), 3000);
      })
      .catch(error => {
        console.error('Error creating order:', error);
        if (error.response) {
          console.log('Response data:', error.response.data);
        }
      });
  };

  
  const handleCancelOrder = () => {
    setIsOrdering(false); // Ukryj formularz zamówienia
  };

  return (
    <div className="App">
      <Header />
      <div className="main-container">
        {isOrdering ? (
       <OrderForm onSubmit={handleSubmitOrder} onCancel={handleCancelOrder} />
        ) : (
          <PizzaList addToCart={addToCart} />
        )}
        <Cart cartItems={cartItems} onCheckout={handleCheckout} />
      </div>
      <NotificationBar message={notificationMessage} show={showNotification} />
    </div>
  );
}

export default App;
