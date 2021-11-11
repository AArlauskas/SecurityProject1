import axios from "axios"

const api = axios.create({
    baseURL: "https://localhost:44314/api",
    headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

api.interceptors.response.use(response => {
    return response;
 }, error => {
   if (error.response.status === 401) {
    localStorage.clear();
    window.location.reload();
   }
   return error;
 });

export const postToDoItem = (data) => api.post("/items", data)

export const getUserItems = () => api.get(`/items`)

export const getAdminItems = () => api.get(`/items/admin`)

export const deleteItem = (id) => api.delete(`/items/${id}`)

export const toggleItem = (id) => api.put(`/items/toggle/${id}`)

export const getItem = (id) => api.get(`/items/${id}`)

export const updateItem = (id, data) => api.put(`/items/${id}`, data)

export const getAdminUsers = () => api.get(`/users/admin`)

export const addUser = (data) => api.post(`/users/admin`, data)

export const updateUser = (data) => api.put(`/users/`, data)

export const deleteUser = () => api.delete(`/users/admin`)