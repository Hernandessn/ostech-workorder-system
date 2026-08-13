import React, { useState, useEffect } from 'react';

import api from '../../services/api';

import { Loading } from '../../components/Loading';
import { ErrorState } from '../../components/ErrorState';
import { EmptyState } from '../../components/EmptyState';

import { CreateCategory, CategoryList, EditCategory, DeleteCategory } from '../../components/CategoryItens';
import { CreateButton } from '../../components/Buttons/CreateButton';
import { Container } from '../../components/Container';
import { Header } from '../../components/Header';

import { toast } from 'react-toastify';
import { validateCategory } from '../../validations/categoryValidation';
import { getApiErrorMessage } from '../../utils/apiError';
import { useRequestState } from '../../hooks/useRequestState';
import { useModals } from '../../hooks/useModals';

export const Category = () => {
    const {
        isLoading,
        setIsLoading,
        isSubmitting,
        setIsSubmitting,
        isError,
        setIsError,
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

    const [category, setCategory] = useState([]);

    const handleChange = e => {
        const { name, value } = e.target;
        setCategorySelected({
            ...categorySelected,
            [name]: value
        });
        console.log(categorySelected);
    };

    const clearCategorySelected = () => {
        setCategorySelected({
            categoryId: '',
            name: '',
            description: ''
        });

    }

    const getCategory = async () => {
        setIsError(false);
        setIsLoading(true);
        try {
            const response = await api.get('/category');

            setCategory(response.data);
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsLoading(false);
        }
    };

    const postCategory = async () => {
        try {
            const validationErrors = validateCategory(categorySelected);

            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }
            setErrors({});
            setIsSubmitting(true);

            const response = await api.post('/category', {
                name: categorySelected.name,
                description: categorySelected.description
            });

            setCategory(prev => [...prev, response.data]);

            setCategorySelected({
                categoryId: '',
                name: '',
                description: ''
            });

            clearCategorySelected();
            closeCreate();
            toast.success("Categoria criada com sucesso!")
        } catch (error) {
            console.log(error);
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsSubmitting(false);
        }
    };

    const putCategory = async () => {
        try {
            const validationErrors = validateCategory(categorySelected);

            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }
            setErrors({});
            setIsSubmitting(true);

            const response = await api.put(
                `/category/${categorySelected.categoryId}`,
                categorySelected
            );

            setCategory(prev =>
                prev.map(item =>
                    item.categoryId === response.data.categoryId
                        ? response.data
                        : item
                )
            );
            clearCategorySelected();
            closeEdit();
            toast.success("Atualizações salvas com sucesso!");
        } catch (error) {
            console.log(error);
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsSubmitting(false);
        }
    };
    const deleteCategory = async () => {
        setIsSubmitting(true);
        try {
            await api.delete(
                `/category/${categorySelected.categoryId}`
            );

            setCategory(prev =>
                prev.filter(
                    item =>
                        item.categoryId !== categorySelected.categoryId
                )
            );
            clearCategorySelected();
            closeDelete();
            toast.success("Categoria deletada com sucesso!");
        } catch (error) {
            console.log(error);
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsSubmitting(false)
        }
    };

    useEffect(() => {
        getCategory();
    }, []);



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
                                    isSubmitting={isSubmitting}
                                    onSubmit={postCategory}
                                    errors={errors}
                                />
                                <EditCategory
                                    category={categorySelected}
                                    isOpen={isEditOpen}
                                    onClose={closeEdit}
                                    onChange={handleChange}
                                    isSubmitting={isSubmitting}
                                    onSubmit={putCategory}
                                    errors={errors}
                                />
                                <DeleteCategory
                                    category={categorySelected}
                                    isOpen={isDeleteOpen}
                                    onClose={closeDelete}
                                    isSubmitting={isSubmitting}
                                    onConfirm={deleteCategory}
                                />
                            </section>
                        )
                    )}
                </div>
            )}
        </Container>
    );
}