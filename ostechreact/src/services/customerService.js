import api from "./api";

export const customerService = {
    getAll: () => api.get("/customer"),

    create: (data) => api.post("/customer", data),

    update: (id, data) => api.put(`/customer/${id}`, data),

    delete: (id) => api.delete(`/customer/${id}`)
};