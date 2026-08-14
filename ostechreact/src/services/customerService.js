import api from "./api";

export const customerService = {
    getAll: async () => {
        const response = await api.get("/customer");

        return response.data;
    },

    create: async (data) => {
        const response = await api.post("/customer", data);

        return response.data;
    },

    update: async (id, data) => {
        const response = await api.put(`/customer/${id}`, data);

        return response.data;
    },

    delete: async (id) => {
        await api.delete(`/customer/${id}`)
    }
};