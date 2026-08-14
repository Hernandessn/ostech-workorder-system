import api from "./api";

export const technicianService = {
    getAll: async () => {
        const response = await api.get("/technician");

        return response.data;
    },

    create: async (data) => {
        const response = await api.post("/technician", data);
        
        return response.data;
    },

    update: async (id, data) => {
        const response = await api.put(`/technician/${id}`, data);

        return response.data;
    },

    delete: async (id) => {
        await api.delete(`/technician/${id}`)
    }
};