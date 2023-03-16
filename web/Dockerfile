FROM node:19-alpine
WORKDIR /app

COPY package*.json .
RUN npm install
COPY . ./

CMD ["npm","run", "start", "--", "--poll", "2000", "--disable-host-check", "--host", "0.0.0.0"]