import React from 'react';
import './Header.css'; // Zaimportuj styl do komponentu

const Header = () => {
  return (
    <div className="header">
      <div className="logo">Pizza na telefon</div>
      <input type="text" placeholder=" " className="search-bar" />
      <div className=""></div>
    </div>
  );
};

export default Header;
