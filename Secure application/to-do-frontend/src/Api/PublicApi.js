import axios from "axios"

const api = axios.create({
    baseURL: "https://localhost:44314/api",
    Headers: {
        "Content-Type": "application/json",
    }
})

export const authenticate = (username, password) => api.post("/auth", {username, password})

export const getCurrentUser = (token) => api.get("/users/personal", { headers: { Authorization: `Bearer ${token}` } })
