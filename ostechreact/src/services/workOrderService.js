import api from "./api";

export const workOrderService = {
    getAll: async () => {
        const response = await api.get("/workOrder");

        return response.data;
    },

    create: async (data) => {
        const response = await api.post("/workOrder", data);

        return response.data;
    },

    update: async (id, data) => {
        const response = await api.put(`/workOrder/${id}`, data);

        return response.data;
    },

    delete: async (id) => {
        await api.delete(`/workOrder/${id}`)
    }
};