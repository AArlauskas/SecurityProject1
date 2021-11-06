import axios from "axios"

const api = axios.create({
    baseURL: "https://localhost:44314/api",
    Headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

axios.interceptors.response.use(response => {
    return response;
 }, error => {
   if (error.response.status === 401) {
       console.log("401 is bad")
    // localStorage.clear();
    // window.location.reload();
   }
   return error;
 });

export const postToDoItem = (data) => api.post("/items", data)

export const getUserItems = (id) => api.get(`/items/`, {params: {userid: id}})

export const getAdminItems = (id) => api.get(`/items/admin/${id}`)

export const deleteItem = (id) => api.delete(`/items/${id}`)

export const toggleItem = (id) => api.put(`/items/toggle/${id}`)

export const getItem = (id) => api.get(`/items/${id}`)

export const updateItem = (id, data) => api.put(`/items/${id}`, data)

export const getAdminUsers = (id) => api.get(`/users/admin/${id}`)

export const addUser = (adminId, data) => api.post(`/users/${adminId}`, data)

export const updateUser = (adminId, data) => api.put(`/users/${adminId}`, data)

export const deleteUser = (adminId, userId) => api.delete(`/users/${adminId}/${userId}`)