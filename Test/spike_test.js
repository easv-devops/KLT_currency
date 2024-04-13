import http from 'k6/http';
import {sleep } from 'k6';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        {duration: '10s', target: 10},
        {duration: '1m', target: 10},
        {duration: '10s', target: 50},
        {duration: '3m', target: 50},
        {duration: '10s', target: 100},
        {duration: '3m', target: 100},
        {duration: '10m', target: 0},

    ]
};


export default () => {
    http.get('http://localhost:5100/currency/get/');
    sleep(1);
}


