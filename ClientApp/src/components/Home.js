import {
    Card, CardImg, CardImgOverlay, CardTitle, CardText
} from 'reactstrap';
import { InputGroup, InputGroupText, Input } from 'reactstrap';

const Home = () => {
    return (
        <>
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
        </>
    );
}

export default Home;