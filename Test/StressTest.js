import http from 'k6/http';
import {sleep } from 'k6';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        {duration: '2m', target: 10},
        {duration: '5m', target: 10},
        {duration: '2m', target: 20},
        {duration: '5m', target: 20},
        {duration: '2m', target: 10},
        {duration: '5m', target: 10},

    ]
};


export default () => {
    http.get('http://localhost:5100:8080/currency/get?testing=true');
    sleep(1);
}


