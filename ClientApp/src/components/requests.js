import axios from 'axios';

const getAllJobs = () => {
    const response = axios.get('/job');
    console.log(response);
    response.then(res => {
        console.log(typeof res)
    })
        
}

export default getAllJobs;