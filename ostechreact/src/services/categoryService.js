import api from "./api";


export const categoryService = {
    getAll: () => api.get("/category"),

    create: (data) => api.post("/category", data),

    update: (id, data) => api.put(`/category/${id}`, data),

    delete: (id) => api.delete(`/category/${id}`)
};