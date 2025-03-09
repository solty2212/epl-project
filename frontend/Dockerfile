# Вихідний образ з Node.js для білду
FROM node:22 AS build
WORKDIR /app

# Копіюємо package.json та встановлюємо залежності
COPY package.json package-lock.json ./
RUN npm install

# Копіюємо весь проєкт і білдимо
COPY . .
RUN npm run build

# Вихідний образ з Nginx для запуску фронтенду
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html

# Копіюємо файл конфігурації Nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Відкриваємо порт
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
