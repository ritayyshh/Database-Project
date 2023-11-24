// Login.js
import axios from 'axios';
import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { useAuth } from '../modules/AuthContext';

const Login = () => {
    const { login } = useAuth();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = (event) => {
        event.preventDefault()

        axios.get('/user').then(res => {
            console.log(res.data);
        })
    }

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
