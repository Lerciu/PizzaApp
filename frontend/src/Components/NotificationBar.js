// NotificationBar.js
import React from 'react';
import './NotificationBar.css'; // Utwórz i zaimportuj odpowiednie style CSS

const NotificationBar = ({ message, show }) => {
  return (
    <div className={`notification-bar ${show ? 'show' : ''}`}>
      {message}
    </div>
  );
};

export default NotificationBar;
