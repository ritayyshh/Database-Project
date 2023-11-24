// Login.js
import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { useAuth } from '../modules/AuthContext';

const Login = () => {
    const { login } = useAuth();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async () => {
        try {
            // Call your authentication API to get a token
            const response = await fetch('your_backend_login_endpoint', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, password }),
            });

            if (response.ok) {
                const { token } = await response.json();
                login(token);
            } else {
                // Handle login error
            }
        } catch (error) {
            console.error('Login error:', error);
        }
    };

    return (
        <Form>
            <FormGroup>
                <Label for="username">Username</Label>
                <Input
                    type="text"
                    id="username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
            </FormGroup>
            <FormGroup>
                <Label for="password">Password</Label>
                <Input
                    type="password"
                    id="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
            </FormGroup>
            <Button color="primary" onClick={handleLogin}>
                Login
            </Button>
        </Form>
    );
};

export default Login;
