import { useState } from "react";

export const useRequestState = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [isError, setIsError] = useState(false);
    const [errors, setErrors] = useState({});

    return {
        isLoading,
        setIsLoading,
        isSubmitting,
        setIsSubmitting,
        isError,
        setIsError,
        errors,
        setErrors
    };
};