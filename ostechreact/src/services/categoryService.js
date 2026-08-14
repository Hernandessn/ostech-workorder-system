import api from "./api";


export const categoryService = {
    getAll: async () => {
        const response = await api.get("/category");

        return response.data;
    },

    create: async (data) => {
        const response = await api.post("/category", data);

        return response.data;
    },

    update: async (id, data) => {
        const response = await api.put(`/category/${id}`, data);

        return response.data;
    },

    delete: async (id) => {
        await api.delete(`/category/${id}`);
    }
};