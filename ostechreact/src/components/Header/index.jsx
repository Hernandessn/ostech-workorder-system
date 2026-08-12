import logoOstech from '../../assets/logo-ostech.png';

export const Header = () => {
    return (
        <header className="flex items-center gap-4 px-6 py-4 bg-[#03346E] text-[#E2E2B6]">
            <img src={logoOstech} alt="Logo OSTech" className="h-10 w-auto" />
            <h2 className="text-lg">
                Bem vindo, <strong className="text-[#6EACDA]">Hernandes</strong>!
            </h2>
        </header>
    )
}