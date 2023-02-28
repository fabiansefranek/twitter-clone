FROM node:19-alpine
WORKDIR /app

COPY package*.json .
COPY . ./

CMD ["npm", "start", "--", "--poll", "2000", "--disable-host-check", "--host", "0.0.0.0"]