import api from "./api";

export const workOrderService = {
    getAll: () => api.get("/workOrder"),

    create: (data) => api.post("/workOrder", data),

    update: (id, data) => api.put(`/workOrder/${id}`, data),

    delete: (id) => api.delete(`/workOrder/${id}`)
};