import { Link } from 'react-router-dom';

export const NotFound = () => {
    return (
        <div className="min-h-screen flex flex-col items-center justify-center gap-4 bg-[#021526] px-4">
            <span className="text-sm uppercase tracking-widest text-[#6EACDA]/70">
                Erro 404
            </span>

            <h1 className="text-7xl font-bold text-[#6EACDA]">
                404
            </h1>

            <p className="text-[#E2E2B6] text-center max-w-sm">
                A página que você está procurando não existe ou foi movida.
            </p>

            <Link
                to="/"
                className="mt-2 px-5 py-2.5 rounded-md bg-[#03346E] text-[#E2E2B6] font-medium hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
            >
                Voltar para o início
            </Link>
        </div>
    );
};