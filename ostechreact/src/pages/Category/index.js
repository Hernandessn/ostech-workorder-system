import { useState, useEffect } from 'react';
import api from '../../services/api';
import logoOstech from '../../assets/logo-ostech.png';
import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import './styles.css'

export const Category = () => {

    const [category, setCategory] = useState([]);

    useEffect(() => {
        api.get('v1/category')
            .then(response => {
                console.log(response.data);
                setCategory(response.data);
            })
            .catch(err => console.error(err));
    }, []);

    return (
        <div className="category-container">
            <header>
                <img src={logoOstech} alt='Logo OSTech' />
                <h2>Bem vindo!<strong> Hernandes</strong>!</h2>
            </header>
            <div className='category-item'>
            <h1>Category List</h1>
            <button type='button' className='category-button'>
                <PlusIcon size={22} />
                Create Category
            </button>
            </div>
            <ul className="category-list">
                {category.map(value => (
                    <li key={value.id} className="category-item">
                        <div className="category-info">
                            <p className="category-name">{value.name}</p>
                            <p className="category-description">{value.description}</p>
                        </div>
                        <div className="category-actions">
                            <button type="button" className="btn-icon btn-edit">
                                <PencilSimpleIcon size={22} />
                            </button>
                            <button type="button" className="btn-icon btn-delete">
                                <TrashIcon size={22} />
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
}