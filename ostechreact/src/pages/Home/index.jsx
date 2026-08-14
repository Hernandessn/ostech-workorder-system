import { Link } from 'react-router-dom';
import { Container } from '../../components/Container';
import { Header } from '../../components/Header';

const links = [
    { to: '/categories', label: 'Categories', description: 'Gerencie as categorias de serviço' },
    { to: '/customers', label: 'Customers', description: 'Gerencie os clientes' },
    { to: '/equipments', label: 'Equipments', description: 'Gerencie os equipamentos' },
    { to: '/technicians', label: 'Technicians', description: 'Gerencie os técnicos' },
    { to: '/workorders', label: 'Work Orders', description: 'Gerencie as ordens de serviço' },
];

export const Home = () => {
    return (
        <Container>
            <Header />
            <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                <h1 className="text-xl font-semibold text-[#E2E2B6]">Menu</h1>
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                    {links.map(link => (
                        <Link
                            key={link.to}
                            to={link.to}
                            className="rounded-md bg-[#03346E] text-[#E2E2B6] px-4 py-4 hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
                        >
                            <p className="font-semibold text-lg">{link.label}</p>
                            <p className="text-sm opacity-80">{link.description}</p>
                        </Link>
                    ))}
                </div>
            </section>
        </Container>
    );
}