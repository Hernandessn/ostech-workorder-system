import api from "./api";

export const technicianService = {
    getAll: () => api.get("/technician"),

    create: (data) => api.post("/technician", data),

    update: (id, data) => api.put(`/technician/${id}`, data),

    delete: (id) => api.delete(`/technician/${id}`)
};