import api from "./api";

export const equipmentService = {
    getAll: async () => {
        const response = await api.get("/equipment");

        return response.data;
    },

    create: async (data) => {
        const response = await api.post("/equipment", data)

        return response.data;
    },

    update: async (id, data) => {
        const response = await api.put(`/equipment/${id}`, data);

        return response.data;
    },

    delete: async (id) => {
        await api.delete(`/equipment/${id}`)
    }
};