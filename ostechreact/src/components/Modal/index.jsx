export const Modal = ({ isOpen, onClose, title, children }) => {
    if (!isOpen) {
        return null;
    }

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/60">
            <div className="w-full max-w-md rounded-lg bg-[#03346E] text-[#E2E2B6] shadow-xl">
                <div className="flex items-center justify-between px-6 py-4 border-b border-[#6EACDA]/30">
                    <h2 className="text-lg font-semibold">{title}</h2>
                    <button
                        onClick={onClose}
                        className="text-2xl leading-none hover:text-[#6EACDA] transition-colors"
                    >
                        ×
                    </button>
                </div>
                <div className="px-6 py-4">
                    {children}
                </div>
            </div>
        </div>
    );
};