export const getApiErrorMessage = (error) => {
    if (!error.response) {
        return "Não foi possível conectar ao servidor.";
    }

    switch (error.response.status) {
        case 400:
            return "Os dados enviados são inválidos.";

        case 404:
            return "O registro solicitado não foi encontrado.";

        case 409:
            return "Não foi possível realizar a operação porque existe um conflito.";

        case 500:
            return "Ocorreu um erro interno no servidor.";

        default:
            return "Ocorreu um erro inesperado.";
    }
};