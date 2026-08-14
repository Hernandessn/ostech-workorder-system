import { useEffect, useState } from 'react';
import api from '../../services/api';
import { toast } from 'react-toastify';
import { Container } from '../../components/Container';
import { Header } from '../../components/Header';
import { Loading } from '../../components/Loading';
import { getApiErrorMessage } from '../../utils/apiError';

const StatCard = ({ label, value }) => (
    <div className="rounded-md bg-[#03346E] px-5 py-4 flex flex-col gap-1">
        <span className="text-sm text-[#6EACDA]">{label}</span>
        <span className="text-3xl font-semibold text-[#E2E2B6]">{value}</span>
    </div>
);

export const Dashboard = () => {
    const [isLoading, setIsLoading] = useState(true);
    const [isError, setIsError] = useState(false);

    const [counts, setCounts] = useState({
        customers: 0,
        technicians: 0,
        equipments: 0,
        categories: 0,
        workOrders: 0,
    });

    const [workOrdersByStatus, setWorkOrdersByStatus] = useState({});
    const [recentWorkOrders, setRecentWorkOrders] = useState([]);

    useEffect(() => {
        const loadDashboard = async () => {
            setIsError(false);
            setIsLoading(true);
            try {
                const [custRes, techRes, equipRes, catRes, woRes] = await Promise.all([
                    api.get('/customer'),
                    api.get('/technician'),
                    api.get('/equipment'),
                    api.get('/category'),
                    api.get('/workorder'),
                ]);

                setCounts({
                    customers: custRes.data.length,
                    technicians: techRes.data.length,
                    equipments: equipRes.data.length,
                    categories: catRes.data.length,
                    workOrders: woRes.data.length,
                });

                const statusGroups = woRes.data.reduce((acc, wo) => {
                    const status = wo.status?.trim() ? wo.status : 'Sem status';
                    acc[status] = (acc[status] || 0) + 1;
                    return acc;
                }, {});
                setWorkOrdersByStatus(statusGroups);

                const sorted = [...woRes.data]
                    .sort((a, b) => new Date(b.openingDate) - new Date(a.openingDate))
                    .slice(0, 5);
                setRecentWorkOrders(sorted);
            } catch (error) {
                setIsError(true);
                toast.error(getApiErrorMessage(error));
            } finally {
                setIsLoading(false);
            }
        };

        loadDashboard();
    }, []);

    return (
        <Container>
            <Header />
            <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                <h1 className="text-xl font-semibold text-[#E2E2B6]">Dashboard</h1>

                {isLoading ? (
                    <Loading />
                ) : isError ? (
                    <p className="text-red-500">Não foi possível carregar os dados do dashboard.</p>
                ) : (
                    <>
                        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4">
                            <StatCard label="Customers" value={counts.customers} />
                            <StatCard label="Technicians" value={counts.technicians} />
                            <StatCard label="Equipments" value={counts.equipments} />
                            <StatCard label="Categories" value={counts.categories} />
                            <StatCard label="Work Orders" value={counts.workOrders} />
                        </div>

                        <div className="rounded-md bg-[#03346E] px-5 py-4 flex flex-col gap-3">
                            <h2 className="text-lg font-semibold text-[#E2E2B6]">Work Orders por status</h2>

                            {Object.keys(workOrdersByStatus).length === 0 ? (
                                <p className="text-sm text-[#E2E2B6]/70">Nenhuma ordem de serviço cadastrada.</p>
                            ) : (
                                <div className="flex flex-col gap-2">
                                    {Object.entries(workOrdersByStatus).map(([status, count]) => (
                                        <div key={status} className="flex items-center justify-between text-sm">
                                            <span className="text-[#E2E2B6]">{status}</span>
                                            <span className="text-[#6EACDA] font-medium">{count}</span>
                                        </div>
                                    ))}
                                </div>
                            )}
                        </div>

                        <div className="rounded-md bg-[#03346E] px-5 py-4 flex flex-col gap-3">
                            <h2 className="text-lg font-semibold text-[#E2E2B6]">Últimas ordens de serviço</h2>

                            {recentWorkOrders.length === 0 ? (
                                <p className="text-sm text-[#E2E2B6]/70">Nenhuma ordem de serviço recente.</p>
                            ) : (
                                <div className="flex flex-col gap-2">
                                    {recentWorkOrders.map(wo => (
                                        <div
                                            key={wo.workOrderId}
                                            className="flex flex-col sm:flex-row sm:items-center sm:justify-between text-sm border-b border-[#6EACDA]/10 pb-2 last:border-0 last:pb-0"
                                        >
                                            <span className="text-[#E2E2B6]">{wo.title}</span>
                                            <span className="text-[#6EACDA]">{wo.openingDate}</span>
                                        </div>
                                    ))}
                                </div>
                            )}
                        </div>
                    </>
                )}
            </section>
        </Container>
    );
}