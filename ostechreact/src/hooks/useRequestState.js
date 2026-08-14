import { useState } from "react";

export const useRequestState = () => {
    const [errors, setErrors] = useState({});

    return {
        errors,
        setErrors
    };
};