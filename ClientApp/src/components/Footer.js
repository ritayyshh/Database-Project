import { Container, Row, Col } from 'reactstrap';

const Footer = () => {
    return (
        <footer className="bg-dark text-light mt-5" style={{ padding: 30 }}>
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
            <div className="text-center" style={{ height: 20 }}>
                Copyright &copy; 2023 Jobex
                <br></br>
            </div>
        </footer>
    )
}

export default Footer;