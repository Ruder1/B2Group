FROM node:latest as build
WORKDIR /usr/local/app
COPY package.json package-lock.json ./
COPY . .
RUN npm install
RUN npm run build

FROM nginx:1.25.4-alpine
COPY --from=build /usr/local/app/dist/client-app /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80