import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { AuthProvider } from './modules/AuthContext';
import 'bootstrap/dist/css/bootstrap.min.css';
import './StyleSheet.css'
import NavBar from './components/NavBar';
import Footer from './components/Footer';
//import Hello from './components/hello';
import Home from './components/Home'; 
import Login from './components/Login';

const App = (args) => {
    return (
        <div>
            <AuthProvider>
                <Router>
                    <NavBar />
                    <Routes>
                        <Route path="/login" element={<Login />} />
                        {/*<Route path="/signup" component={Signup} />*/}
                        <Route path="/" element={<Home />} />
                    </Routes>
                    <Footer />
                </Router>
            </AuthProvider>
            
        </div>
    );
};

export default App;