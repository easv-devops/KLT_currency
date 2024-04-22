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
        {duration: '5m', target: 0},

    ],
    
    thresholds: {
        // You can define thresholds for your test here
        http_req_duration: ['p(95)<500'], // 95% of requests should be below 500ms
    },
};


export default () => {
    http.get('http://144.91.64.53:5100/currency/get?testing=true');
    sleep(1);
}


