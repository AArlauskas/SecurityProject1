import axios from "axios"

const api = axios.create({
    baseURL: "https://localhost:44314/api",
    Headers: {
        "Content-Type": "application/json",
    }
})

export const login = (username, password) => api.post("/users/login", {username, password})

export const postToDoItem = (data) => api.post("/items", data)

export const getUserItems = (id) => api.get(`/items/${id}`)

export const getAdminItems = (id) => api.get(`/items/admin/${id}`)

export const deleteItem = (id) => api.delete(`/items/${id}`)

export const toggleItem = (id) => api.patch(`items/toggle/${id}`)