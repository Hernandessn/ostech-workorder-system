import { Modal } from '../Modal';

export const CreateEquipment = ({
    equipment,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Create equipment">
            <div className="flex flex-col gap-3">
                <label htmlFor="cat-name" className="text-sm font-medium">Name:</label>
                <input
                    id="cat-name"
                    type="text"
                    name="name"
                    value={equipment.name}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-brand" className="text-sm font-medium">Brand:</label>
                <input
                    id="cat-brand"
                    type="text"
                    name="brand"
                    value={equipment.brand}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-model" className="text-sm font-medium">Model:</label>
                <input
                    id="cat-model"
                    type="text"
                    name="model"
                    value={equipment.model}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-serialNumber" className="text-sm font-medium">Serial Number:</label>
                <input
                    id="cat-serialNumber"
                    type="text"
                    name="serialNumber"
                    value={equipment.serialNumber}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
            </div>

            <div className="flex justify-end gap-2 mt-4">
                <button
                    onClick={onSubmit}
                    disabled={isSubmitting}
                    className="px-4 py-2 rounded-md bg-[#03346E] text-[#E2E2B6] hover:bg-[#6EACDA] hover:text-[#021526] transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                    {isSubmitting ? 'Adding...' : 'Add'}
                </button>

                <button
                    onClick={onClose}
                    className="px-4 py-2 rounded-md bg-red-600 text-white hover:bg-red-700 transition-colors"
                >
                    Cancel
                </button>
            </div>
        </Modal>
    );
};