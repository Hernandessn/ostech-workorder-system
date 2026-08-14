import React, { useState, useEffect, } from 'react';

import {
    CreateCategory,
    CategoryList,
    EditCategory,
    DeleteCategory
} from '../../components/CategoryItens';

import {
    Container,
    Header,
    Loading,
    ErrorState,
    EmptyState
} from '../../components';

import { toast } from 'react-toastify';
import { validateCategory } from '../../validations/categoryValidation';
import { getApiErrorMessage } from '../../utils/apiError';
import { CreateButton } from '../../components/Buttons/CreateButton';

import {
    useModals,
    useRequestState
} from '../../hooks';

import { categoryService } from '../../services/categoryService';

import {
    useMutation,
    useQuery,
    useQueryClient
} from '@tanstack/react-query';

export const Category = () => {
    const {
        errors,
        setErrors
    } = useRequestState();

    const {
        isCreateOpen,
        isEditOpen,
        isDeleteOpen,
        openCreate,
        closeCreate,
        openEdit,
        closeEdit,
        openDelete,
        closeDelete
    } = useModals();

    const [categorySelected, setCategorySelected] = useState({
        categoryId: '',
        name: '',
        description: ''
    });

    const queryClient = useQueryClient();

    const handleChange = e => {
        const { name, value } = e.target;
        setCategorySelected({
            ...categorySelected,
            [name]: value
        });
    };

    const handleCreateCategory = () => {
        const validationErrors = validateCategory(categorySelected);

        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        setErrors({});
        createCategoryMutation.mutate({
            name: categorySelected.name,
            description: categorySelected.description
        });
    };

    const handleUpdateCategory = () => {
        updateCategoryMutation.mutate({
            id: categorySelected.categoryId,
            data: categorySelected
        });
    };

    const handleDeleteCategory = () => {
        deleteCategoryMutation.mutate(
            categorySelected.categoryId
        );
    };

    const clearCategorySelected = () => {
        setCategorySelected({
            categoryId: '',
            name: '',
            description: ''
        });
    };

    const {
        data: category = [],
        isLoading,
        isError,
        error
    } = useQuery({
        queryKey: ["categories"],
        queryFn: categoryService.getAll
    });


    const createCategoryMutation = useMutation({
        mutationFn: categoryService.create,
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ["categories"]
            });

            closeCreate();
            clearCategorySelected();

            toast.success("Categoria criada com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const updateCategoryMutation = useMutation({
        mutationFn: ({ id, data }) =>
            categoryService.update(id, data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["categories"]
            });

            closeEdit();
            clearCategorySelected();

            toast.success("Atualizações salvas com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const deleteCategoryMutation = useMutation({
        mutationFn: categoryService.delete,

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["categories"]
            });

            closeDelete();
            clearCategorySelected();

            toast.success("Categoria deletada com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    useEffect(() => {
        if (isError) {
            toast.error(getApiErrorMessage(error));
        }
    }, [isError, error]);

    return (
        <Container>
            {isError ? (
                <ErrorState message="Erro ao carregar a lista, tente novamente!" />
            ) : (
                <div>
                    {isLoading ? (
                        <Loading />
                    ) : (
                        category.length === 0 ? (
                            <div className="flex flex-col items-center justify-center gap-4 py-16">
                                <EmptyState message="A lista está vazia, crie uma categoria: " />
                                <CreateButton
                                    entity="Category"
                                    onCreate={() => {
                                        clearCategorySelected()
                                        setErrors({});
                                        openCreate();
                                    }}
                                />
                            </div>
                        ) : (
                            <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                                <Header />
                                <div className="flex items-center justify-between">
                                    <h1 className="text-xl font-semibold text-[#E2E2B6]">Category List</h1>
                                    <CreateButton
                                        entity="Category"
                                        onCreate={() => {
                                            clearCategorySelected()
                                            setErrors({});
                                            openCreate();

                                        }}
                                    />
                                </div>
                                <ul className="flex flex-col gap-3">
                                    {category.map(value => (
                                        <CategoryList
                                            key={value.categoryId}
                                            category={value}
                                            onEdit={() => {
                                                setCategorySelected(value)
                                                openEdit();
                                            }}
                                            onDelete={() => {
                                                setCategorySelected(value)
                                                openDelete();
                                            }} />
                                    ))}
                                </ul>
                                <CreateCategory
                                    category={categorySelected}
                                    isOpen={isCreateOpen}
                                    onClose={closeCreate}
                                    onChange={handleChange}
                                    isSubmitting={createCategoryMutation.isPending}
                                    onSubmit={handleCreateCategory}
                                    errors={errors}
                                />
                                <EditCategory
                                    category={categorySelected}
                                    isOpen={isEditOpen}
                                    onClose={closeEdit}
                                    onChange={handleChange}
                                    isSubmitting={updateCategoryMutation.isPending}
                                    onSubmit={handleUpdateCategory}
                                    errors={errors}
                                />
                                <DeleteCategory
                                    category={categorySelected}
                                    isOpen={isDeleteOpen}
                                    onClose={closeDelete}
                                    isSubmitting={deleteCategoryMutation.isPending}
                                    onConfirm={handleDeleteCategory}
                                />
                            </section>
                        )
                    )}
                </div>
            )}
        </Container>
    );
}