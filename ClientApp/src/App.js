import React, { useState } from 'react';
import {
    Card, CardImg, CardImgOverlay, CardTitle, CardText
} from 'reactstrap';
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
import { InputGroup, InputGroupText, Input} from 'reactstrap';
import { Container, Row, Col } from 'reactstrap';
import 'bootstrap/dist/css/bootstrap.min.css';


const App = (args) => {

    const [isOpen, setIsOpen] = useState(false);

    const toggle = () => setIsOpen(!isOpen);
    return (
        <div>
            <Navbar {...args}>
                <NavbarBrand href="/">Jobex</NavbarBrand>
                <NavbarToggler onClick={toggle} />
                <Collapse isOpen={isOpen} navbar>
                    <Nav className="me-auto" navbar>
                        <NavItem>
                            <NavLink href="/components/">Components</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink href="https://github.com/reactstrap/reactstrap">
                                GitHub
                            </NavLink>
                        </NavItem>
                        <UncontrolledDropdown nav inNavbar>
                            <DropdownToggle nav caret>
                                Options
                            </DropdownToggle>
                            <DropdownMenu right>
                                <DropdownItem>Option 1</DropdownItem>
                                <DropdownItem>Option 2</DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>Reset</DropdownItem>
                            </DropdownMenu>
                        </UncontrolledDropdown>
                    </Nav>
                    <NavbarText>Simple Text</NavbarText>
                </Collapse>
            </Navbar>
            <Card inverse>
                <CardImg
                    alt="Card image cap"
                    src="https://picsum.photos/900/270?grayscale"
                    style={{
                        height: 270
                    }}
                    width="100%"
                />
                <CardImgOverlay>
                    <CardTitle tag="h5" style={{ color: '#FFFFFF', fontSize: 40 }}>
                        Jobex
                    </CardTitle>
                    <CardText style={{ color: '#FFFFFF', fontSize: 20 }}>
                        Get Your Dream Job
                    </CardText>
                </CardImgOverlay>
            </Card>
            <div>
                <InputGroup>
                    <InputGroupText>
                        Job Title
                    </InputGroupText>
                    <Input placeholder="Enter Job Title of your choice" />
                </InputGroup>
                <br />
                <InputGroup>
                    <InputGroupText>
                        Location
                    </InputGroupText>
                    <Input placeholder="Enter Location" />
                </InputGroup>
                <br />
                <br />
            </div>
            <footer className="bg-dark text-light mt-5" style={{padding: 30} }>
                <Container>
                    <Row>
                        <Col xs={12} md={4}>
                            <h4>Contact Us</h4>
                            <ul>
                                <li>company@email.com</li>
                                <li>+1 234 567 8900</li>
                                <li>123 Main Street, Anytown, USA</li>
                            </ul>
                        </Col>
                        <Col xs={12} md={4}>
                            <h4>Links</h4>
                            <ul>
                                <li><a href="#">About Us</a></li>
                                <li><a href="#">Services</a></li>
                                <li><a href="#">Blog</a></li>
                                <li><a href="#">Contact</a></li>
                            </ul>
                        </Col>
                        <Col xs={12} md={4}>
                            <h4>Social</h4>
                            <ul>
                                <li><a href="#"><i className="fa fa-facebook-square"></i> Facebook</a></li>
                                <li><a href="#"><i className="fa fa-twitter"></i> Twitter</a></li>
                                <li><a href="#"><i className="fa fa-linkedin-square"></i> LinkedIn</a></li>
                                <li><a href="#"><i className="fa fa-instagram"></i> Instagram</a></li>
                            </ul>
                        </Col>
                    </Row>
                </Container>
                <hr />
                <div className="text-center" style={{height: 20}}>
                    Copyright &copy; 2023 Jobex
                    <br></br>
                </div>
            </footer>
        </div>
    );
};

export default App;