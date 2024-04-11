# Stage 1: Build Angular application
FROM node:18 AS build
WORKDIR /app
COPY . /app
RUN npm install
RUN npm run build --prod

# Stage 2: Setup Nginx and serve built Angular application
FROM nginx:alpine
COPY --from=build /app/www /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
