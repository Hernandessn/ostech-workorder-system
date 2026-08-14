import { Link, useLocation } from 'react-router-dom';
import { HouseIcon } from '@phosphor-icons/react';
import logoOstech from '../../assets/logo-ostech.png';

export const Header = () => {
    const location = useLocation();
    const isHome = location.pathname === '/';

    return (
        <header className="flex items-center justify-between px-6 py-4 bg-[#03346E] text-[#E2E2B6]">
            <div className="flex items-center gap-4">
                <img src={logoOstech} alt="Logo OSTech" className="h-10 w-auto" />
                <h2 className="text-lg">
                    Bem vindo, <strong className="text-[#6EACDA]">Hernandes</strong>!
                </h2>
            </div>

            {!isHome && (
                <Link
                    to="/"
                    className="flex items-center gap-2 px-3 py-2 rounded-md text-sm hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
                >
                    <HouseIcon size={20} />
                    Início
                </Link>
            )}
        </header>
    );
}