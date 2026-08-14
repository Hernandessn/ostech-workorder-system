import { Link, useLocation } from 'react-router-dom';
import { HouseIcon } from '@phosphor-icons/react';
import logoOstech from '../../assets/logo-ostech.png';

export const Header = () => {
    const location = useLocation();
    const isHome = location.pathname === '/';

    return (
        <header className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3 px-4 sm:px-6 py-3 sm:py-4 bg-[#03346E] text-[#E2E2B6]">
            <div className="flex items-center justify-between gap-3">
                <img src={logoOstech} alt="Logo OSTech" className="h-8 sm:h-10 w-auto" />

                {!isHome && (
                    <Link
                        to="/"
                        className="flex sm:hidden items-center gap-2 px-3 py-1.5 rounded-md text-sm hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
                    >
                        <HouseIcon size={18} />
                        Início
                    </Link>
                )}
            </div>

            <h2 className="text-sm sm:text-lg">
                Bem vindo, <strong className="text-[#6EACDA]">Hernandes</strong>!
            </h2>

            {!isHome && (
                <Link
                    to="/"
                    className="hidden sm:flex items-center gap-2 px-3 py-2 rounded-md text-sm hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
                >
                    <HouseIcon size={20} />
                    Início
                </Link>
            )}
        </header>
    );
}