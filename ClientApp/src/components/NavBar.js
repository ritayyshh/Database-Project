import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../modules/AuthContext';
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
    NavbarText,
} from 'reactstrap';

const NavBar = ({ args }) => {
    const { user, logout } = useAuth();
    const [isOpen, setIsOpen] = useState(false);

    const toggle = () => setIsOpen(!isOpen);
    return (
        <Navbar {...args}>
            <NavbarBrand href="/">Jobex</NavbarBrand>
            <NavbarToggler onClick={toggle} />
            <Collapse isOpen={isOpen} navbar>
                <Nav className="me-auto" navbar>
                    {!user ? (
                        <>
                            <>
                                <NavItem>
                                    <Link to="/login">Login</Link>
                                </NavItem>
                                <NavItem>
                                    <Link to="/signup">Sign Up</Link>
                                </NavItem>
                            </>
                        </>
                    ) : (

                            <NavItem>
                                <button onClick={logout}>Logout</button>
                            </NavItem>
                    )}
                    {/*<NavItem>*/}
                    {/*    <NavLink href="/components/">Components</NavLink>*/}
                    {/*</NavItem>*/}
                    {/*<NavItem>*/}
                    {/*    <NavLink href="https://github.com/reactstrap/reactstrap">*/}
                    {/*        GitHub*/}
                    {/*    </NavLink>*/}
                    {/*</NavItem>*/}
                    <UncontrolledDropdown nav inNavbar>
                        <DropdownToggle nav caret>
                            Options
                        </DropdownToggle>
                        <DropdownMenu right>
                            <DropdownItem>Option 2</DropdownItem>
                            <DropdownItem divider />
                            <DropdownItem>Reset</DropdownItem>
                        </DropdownMenu>
                    </UncontrolledDropdown>
                </Nav>
                {user ? (
                    <NavbarText>Hello, {user.username}!</NavbarText>
                ): (null)
                }
                
            </Collapse>
        </Navbar>
    );
}

export default NavBar;